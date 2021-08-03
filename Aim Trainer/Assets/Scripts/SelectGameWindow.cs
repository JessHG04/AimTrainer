using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectGameWindow : MonoBehaviour {
    public void MotionlessButton() {
        SceneManager.LoadScene("Motionless Target Scene", LoadSceneMode.Single);
    }

    public void MovingTargetsButton() {
        SceneManager.LoadScene("Moving Target Scene", LoadSceneMode.Single);
    }

    public void TargetTrackingButton() {
        SceneManager.LoadScene("Target Tracking Scene", LoadSceneMode.Single);
    }

    public void ExitButton() {
        Application.Quit();
    }
}
