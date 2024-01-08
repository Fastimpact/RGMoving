using System.Collections.Generic;
using BananaParty.BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

namespace RunGun.Gameplay
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private CharacterController _controller;
        [SerializeField] private Animator _animator;
        [SerializeField] private LineRenderer _shootLine;
        [SerializeField] private InteractSensor _interactSensor;
        
        [Header("Configs")]
        [SerializeField] private CharacterConfig _config;
        [SerializeField] private MeleeAttackConfig _meleeAttackConfig;
        [SerializeField] private RangeAttackConfig _rangeAttackConfig;
        [SerializeField] private RangeAttackConfig _rangeAimedAttackConfig;
        [SerializeField] private ParryConfig _parryConfig;

        [Header("Consumables")]
        [SerializeField] private HealingMedicine _healingMedicine;

        [Header("View")]
        [SerializeField] private CharacterStyleView _characterStyleView;

        [Header("Debug")]
        [SerializeField] private bool _debug;

        private readonly List<BehaviorNode> _behaviorTrees = new();
        private Vector3 _fallPosition;

        public IStyle Style { get; private set; }

        public IParry Parry { get; private set; }

        public CharacterInput Input { get; private set; }

        public MeleeAttackConfig MeleeAttackConfig => _meleeAttackConfig;

        public RangeAttackConfig RangeAttackConfig => _rangeAttackConfig;
        public RangeAttackConfig RangeAimedAttackConfig => _rangeAimedAttackConfig;
        public CharacterController Controller => _controller;
           
        [field: SerializeField] public Vector3 SpawnPoint { get; private set; }

        [field: SerializeField] public CharacterHealth Health { get; private set; }

        private void Awake()
        {
            transform.position = SpawnPoint;
            Style = new Style(_characterStyleView, _config.StyleMax);
            Input = new CharacterInput();
            Input.Enable();
            IAttackView meleeAttackView = new AttackView(_animator, "Punch");
            IAttackView rangeAutoAttackView = new AttackView(_animator, "Shoot");
            IAttackView rangeAimedAttackView = new AttackView(_animator, "StrShoot");
            IAttackView dashView = new AttackView(_animator, "Dash");
            IAttackView shortDashView = new AttackView(_animator, "ShortDash");
            IAttackView meleeDashView = new AttackView(_animator, "MeleeDash");
            IAbility chargedAbility = new Ability(needStyle: (int)(_config.StyleMax/(float)_config.StyleName));
            
            _healingMedicine.Initialize(Health, Style);
         
            CharacterMovement movement = new CharacterMovement(_controller, _animator, _config.Speed);
            Parry = new Parry(transform, _parryConfig, Style, chargedAbility, _animator);
            Aim aim = new Aim();
            List<AttackConfig> attackConfigs = new List<AttackConfig> { _meleeAttackConfig, _rangeAttackConfig, _rangeAimedAttackConfig };


            IAttack meleeAttack = new AttackWithView(new MeleeAttack(this, movement,
                    chargedAbility,
                    _meleeAttackConfig, Health, Style),
                meleeAttackView);

            IAttack rangeAutoAttack =
                new AttackStyleSpend(new AttackWithView(
                    new RangeAttack( _rangeAttackConfig),
                    rangeAutoAttackView), chargedAbility, Style);

            IAttack rangeAimedAttack =
                new AttackWithView(
                    new RangeAimedAttack(transform, _rangeAimedAttackConfig),
                    rangeAimedAttackView);

            ICombo meleeCombo = new ChargedCombo(new Combo(meleeAttack, Input, _animator, _controller, "isPunch"), chargedAbility, meleeAttackView, Style, _meleeAttackConfig.ChargedConfig);
            ICombo rangeCombo = new ChargedCombo(new Combo(rangeAutoAttack, Input, _animator, _controller, "isShoot"), chargedAbility, rangeAutoAttackView, Style, _rangeAttackConfig.ChargedConfig);
            
            _behaviorTrees.Add(new RepeatNode(new SequenceNode(new IBehaviorNode[]
            {
                new PerformedInputThisFrameNode(Input.Attack.CounterAttack),
                new ParryNode(Parry, _config.ParryTime, _animator),
            })));

            _behaviorTrees.Add(new RepeatNode(new SequenceNode(new IBehaviorNode[]
            {
                new IsInputMovingNode(Input),
                new AimIsNotActiveNode(aim),
                new ParryIsNotActiveNode(Parry),
                new InverterNode(new IsDashingNode(Health)),
                new InverterNode(new IsAbilityActiveNode(chargedAbility)),
                new MoveNode(movement, Input, _controller),
            })));

            _behaviorTrees.Add(new SelectorNode(new IBehaviorNode[]
            {
                new SelectorNode(new IBehaviorNode[]
                {
                    new SequenceNode(new IBehaviorNode[]
                    {
                        new AimIsNotActiveNode(aim),
                        new PerformedInputThisFrameNode(Input.Movement.Dash),
                        new InverterNode(new IsDashingNode(Health)),
                        new DashNode(_controller, Input, _animator, _config.DashForce, dashView, Health, false),
                        new WaitNode(200),
                    }),
                    new SequenceNode(new IBehaviorNode[]
                    {
                        new AimIsNotActiveNode(aim),                       
                        new InverterNode(new IsDashingNode(Health)),
                        new PerformedInputThisFrameNode(Input.Movement.Dash),
                        new DashNode(_controller, Input, _animator, _config.ShortDashForce, shortDashView, Health, true),
                        new IsDashingNode(Health),
                        new DashNode(_controller, Input, _animator, _config.DashForce, dashView, Health, false),                     
                    }),
                }),
                new GravitationNode(_controller, _animator, _fallPosition),
            }));

            _behaviorTrees.Add(new RepeatNode(new SelectorNode(new IBehaviorNode[]
            {
                new SequenceNode(new IBehaviorNode[]
                {
                    new InverterNode(new IsAbilityActiveNode(chargedAbility)),
                    new EnoughStyleNode(Style, chargedAbility, _rangeAimedAttackConfig.StyleSpend),
                    new IsPressingNode(Input.Attack.RangeAttack, 0.25f),
                    new RangeAimedAttackNode(_shootLine, Input, aim, _rangeAimedAttackConfig, rangeAimedAttack, this, _animator),
                    new SpendStyleNode(Style, chargedAbility, _rangeAimedAttackConfig.StyleSpend),
                    new BackToStartNeedStyleAbilityNode(chargedAbility),
                }),
                
                new SequenceNode(new IBehaviorNode[]
                {
                    new SelectorNode(new IBehaviorNode[]
                    {
                        new SequenceNode(new IBehaviorNode[]
                        {
                            new IsAbilityActiveNode(chargedAbility),
                            new PerformedInputThisFrameNode(Input.Attack.RangeAttack),
                        }),
                        new PerformedInputWithoutPressing(Input.Attack.RangeAttack),
                    }),
                    new EnoughStyleNode(Style, chargedAbility, _rangeAttackConfig.StyleSpend),
                    //new AttackNode(_rangeAttackConfig.AttackCooldown, rangeCombo),
                    new BackToStartNeedStyleAbilityNode(chargedAbility),
                }),

                new SequenceNode(new IBehaviorNode[]
                {
                    new IsShootNode(_animator),
                    new PerformedInputThisFrameNode(Input.Attack.MeleeAtack),
                    new SelectorNode(new IBehaviorNode[]
                    {
                        new SequenceNode(new IBehaviorNode []
                        {
                            new IsEnemiesNearNode(transform, 0.5f),
                            new MeleeAttackDash(_controller, _animator, _config.ShortDashForce, _meleeAttackConfig.DamageDistance, meleeDashView),
                            new MeleeComboNode(meleeCombo, chargedAbility, Input),
                        }),
                        
                        new MeleeComboNode(meleeCombo, chargedAbility, Input),
                    }),
                    new BackToStartNeedStyleAbilityNode(chargedAbility),
                }),
            })));

            _behaviorTrees.Add(new RepeatNode(new SequenceNode(new IBehaviorNode[]
            {
                new InverterNode(new IsInputMovingNode(Input)),
                new IdleNode(_animator),
            })));
        } 

        private void Update()
        {
            _behaviorTrees.Update((long)(Time.time * 1000));
        }

        public void SetSpawnPoint(Transform newSpawnPoint)
        {
            SpawnPoint = newSpawnPoint.position;
        }
    }
}