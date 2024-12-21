using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    public static PlayerAnimController Instance { get; private set; }

    [Header("References")]
    IAnimState _currentState;
    [SerializeField] private Animator _animatorBody;
    [SerializeField] private Animator _animatorFoot;

    [Header("Animation Variables")]
    [SerializeField] private bool isWalking;
    [SerializeField] private int isWalkingHash;

    [Header("Getters - Setters")]
    public bool IsWalking { get => isWalking; set => isWalking = value; }
    public Animator AnimatorBody { get => _animatorBody; }
    public Animator AnimatorFoot { get => _animatorFoot; }
    public int IsWalkingHash { get => isWalkingHash; }

    private void Awake()
    {
        #region SingletonPattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        #endregion

        _animatorBody = GetComponent<Animator>();
        _animatorFoot = transform.GetChild(0).GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");

    }

    void Start()
    {
        _currentState = new IdleState();
        _currentState.EnterState(gameObject);
    }

    void Update()
    {
        _currentState.UpdateState(gameObject);
    }

    public void SwitchState(IAnimState state)
    {
        _currentState.ExitState(gameObject);
        _currentState = state;
        _currentState.EnterState(gameObject);
    }

}
