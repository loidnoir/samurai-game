using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public UIController uIController;
    public InputController inputController;
    public Player player;

    private List<Vector2> fingerPath;

    private void Start()
    {
        //var list = Resources.FindObjectsOfTypeAll<Texture>();

        //for (int i = 0; i < list.Length; i++)
        //{
        //    GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        //    quad.name = list[i].name;
        //    quad.GetComponent<MeshRenderer>().material.mainTexture = list[i];
        //    quad.transform.position = new Vector3(i, 10, 0);
        //}
        //foreach (var item in list)
        //{
        //}
    }
    public void StartGame()
    {
        inputController.OnTouchDown.AddListener(OnTouchDown);
        inputController.OnTouch.AddListener(OnTouch);
        inputController.OnTouchUp.AddListener(OnTouchUp);
    }
    public void EndGame()
    {
        inputController.OnTouchDown.RemoveListener(OnTouchDown);
        inputController.OnTouch.RemoveListener(OnTouch);
        inputController.OnTouchUp.RemoveListener(OnTouchUp);
    }

    public void OnTouchDown(Vector2 touchPostion)
    {
        uIController.touchCanvas.Show();
        TimeScaler.timeScaler.Slow(1);
        player.State = Player.StateType.Idle;
    }
    public void OnTouch(Vector2 touchPostion)
    {

    }
    public void OnTouchUp(Vector2 touchPostion)
    {
        uIController.touchCanvas.Hide();
        TimeScaler.timeScaler.Normalize(1);
        player.Attack(UIController.ScreenToWorld(fingerPath, player.transform.position));
    }
}
