using JetBrains.Annotations;
using UnityEngine;

public class MainCharController : MonoBehaviour
{
    string imputUser;
    Time lastImput;

    private void Start()
    {
        Controller();
    }

    private void Update()
    {
        
    }
    public void Controller()
    {
        switch (imputUser)
        {
            case "Up":

                break;
            case "Down":
                break;
            case "Left":
                break;
            case "Right":
                break;
            case "Jump":
                break;
            case "Stop":
                break;
            case "Hit":
                break;
            case "LHit":
                break;
        }
    }


     public void SetImput( string imput){
            imputUser = imput;
     }
}
