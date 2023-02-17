using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private UnityEvent OnAllPlayersJoined;
    [SerializeField] private UnityEvent OnAllPlayersReady;
    [Space]
    [SerializeField] private GameObject prefabControllerIcon;
    [SerializeField] private Sprite ControllerIcon;
    [SerializeField] private Sprite KeyboardIcon;
    [Space]
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
    private void Start()
    {
        GetComponent<GameManager>().OnGameStart += SetUpCharacters;
    }
    private void OnPlayerJoined(PlayerInput joined)
    {
        if (players == null) players = new List<playerSetup>();

        GameObject go = Instantiate(prefabControllerIcon, transform.GetChild(0));
        Sprite s;
        if (joined.currentControlScheme.Contains("Keyboard")) s = KeyboardIcon;
        else s = ControllerIcon;
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
        Debug.Log("All Ready and different selections");
        for (int i = 0; i < players.Count; i++)
        {
            players[i].ControllerImage.gameObject.SetActive(false);
        }
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
        Ready = !Ready;
        ControllerImage.GetChild(0).gameObject.SetActive(Ready);
    }
}
