using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private UnityEvent OnStart;
    [SerializeField] private UnityEvent OnAllPlayersJoined;
    [SerializeField] private UnityEvent OnAllPlayersReady;
    [Space]
    [SerializeField] private GameObject prefabControllerIcon;
    [Header("Sprites")]
    [SerializeField] private Sprite P1ControllerIcon;
    [SerializeField] private Sprite P1KeyboardIcon;
    [Space]
    [SerializeField] private Sprite P2ControllerIcon;
    [SerializeField] private Sprite P2KeyboardIcon;
    [Space]
    [SerializeField] private TMP_Text ToJoinText;
    [Header("More stuff")]
    [SerializeField] private float lowestY;
    [SerializeField] private float yOffset;
    [SerializeField] private float leftPosition;
    [SerializeField] private float rightPosition;
    [Space]
    [SerializeField] private float moveTime;
    [SerializeField] private AnimationCurve moveCurve;
    [Space]
    [SerializeField] private List<CharacterSO> Characters;
    [SerializeField] private Vector3[] spawnPoints = new Vector3[2];
    [Space]
    public List<playerSetup> players;
    private bool allJoined;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        GetComponent<GameManager>().OnGameStart += SetUpCharacters;
        OnStart?.Invoke();
    }
    public void SetUpUI()
    {
        ToJoinText.transform.parent.gameObject.SetActive(true);
        ToJoinText.text = "To Join";
        GetComponent<PlayerInputManager>().EnableJoining();

    }
    private void OnPlayerJoined(PlayerInput joined)
    {
        if (players == null) players = new List<playerSetup>();

        GameObject go = Instantiate(prefabControllerIcon, transform.GetChild(0));
        Sprite s;
        if (players.Count == 0)
        {
            if (joined.currentControlScheme.Contains("Keyboard")) s = P1KeyboardIcon;
            else s = P1ControllerIcon;
        }
        else
        {
            if (joined.currentControlScheme.Contains("Keyboard")) s = P2KeyboardIcon;
            else s = P2ControllerIcon;
        }
        go.GetComponent<Image>().sprite = s;
        RectTransform rect = go.GetComponent<RectTransform>();
        rect.anchoredPosition = new(0, lowestY + yOffset * players.Count);
        rect.GetChild(0).gameObject.SetActive(false);
        playerSetup setup = new(joined.GetComponent<Player>(), joined, joined.gameObject, go.GetComponent<RectTransform>());

        setup.Player.OnCharacterChange += setup.ChangeSelection;
        setup.Player.OnToggleReady += setup.ToggleReady;
        setup.Player.OnCharacterChange += ChangedSelection;
        setup.Player.OnToggleReady += ReadyStateChanged;

        players.Add(setup);

        if (players.Count == 2)
        {
            allJoined = true;
            ToJoinText.text = "To ready up!";
            OnAllPlayersJoined?.Invoke();
            GetComponent<PlayerInputManager>().DisableJoining();
        }
    }
    void ChangedSelection(int changed)
    {
        for (int i = 0; i < players.Count; i++)
        {
            float newPos = 0;
            if (players[i].Selection == -1) newPos = leftPosition;
            else if (players[i].Selection == 1) newPos = rightPosition;
            if (players[i].uiMoveRoutine == null)
                players[i].uiMoveRoutine = StartCoroutine(moveUIOnX(players[i].ControllerImage, newPos, players[i]));
        }
    }
    IEnumerator moveUIOnX(RectTransform rect, float xPosition, playerSetup setup)
    {
        Vector2 originalPosition = rect.anchoredPosition;
        Vector2 newPosition = new(xPosition, originalPosition.y);
        for (float t = 0; t < moveTime; t += Time.deltaTime)
        {
            float v = moveCurve.Evaluate(t / moveTime);
            rect.anchoredPosition = Vector2.MoveTowards(originalPosition, newPosition, Vector2.Distance(originalPosition, newPosition) * v);
            yield return new WaitForEndOfFrame();
        }
        rect.anchoredPosition = newPosition;
        setup.uiMoveRoutine = null;
    }
    void ReadyStateChanged()
    {
        if (players.Count < 2) return;
        List<int> usedSelections = new();
        for (int i = 0; i < players.Count; i++)
        {
            if (!players[i].Ready) return;
            if (players[i].Selection == 0 || usedSelections.Contains(players[i].Selection)) return;
            usedSelections.Add(players[i].Selection);
        }

        for (int i = 0; i < players.Count; i++)
        {
            players[i].ControllerImage.gameObject.SetActive(false);
        }
        transform.GetChild(0).gameObject.SetActive(false);
        OnAllPlayersReady?.Invoke();
    }
    private void ApplyCharacter(Vector3 position, GameObject target, CharacterSO character)
    {
        for (int i = target.transform.childCount - 1; i >= 0; i--)
            Destroy(target.transform.GetChild(i).gameObject);

        Instantiate(character.prefab, position, Quaternion.identity, target.transform);
        target.GetComponent<Player>().SetCharacter(character);
    }
    private void SetUpCharacters()
    {
        GetComponent<PlayerInputManager>().DisableJoining();
        for (int i = 0; i < players.Count; i++)
        {
            players[i].PlayerObject.name = Characters[i].name;
            CharacterSO c;

            if (players[i].Selection == -1) c = Characters[0];
            else c = Characters[1];

            ApplyCharacter(spawnPoints[i], players[i].PlayerObject, c);
            GameManager.Instance.PlayerJoined(players[i].PlayerObject.transform.GetChild(0).gameObject);
            players[i].PlayerInput.SwitchCurrentActionMap("Player");
        }
    }
}
[System.Serializable]
public class playerSetup : object
{
    public bool Ready;
    public int Selection;
    public RectTransform ControllerImage;
    [Space]
    public Player Player;
    public PlayerInput PlayerInput;
    public GameObject PlayerObject;
    [Space]
    public Coroutine uiMoveRoutine;
    public playerSetup(Player playerScript, PlayerInput playerInput, GameObject playerObject, RectTransform icon)
    {
        Player = playerScript;
        PlayerInput = playerInput;
        PlayerObject = playerObject;
        ControllerImage = icon;
    }
    public void ChangeSelection(int val)
    {
        if (!Ready && uiMoveRoutine == null)
            Selection = val;
    }
    public void ToggleReady()
    {
        if (Selection == 0) return;
        Ready = !Ready;
        ControllerImage.GetChild(0).gameObject.SetActive(Ready);
    }
}
