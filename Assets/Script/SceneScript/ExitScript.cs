using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScript : BaseMono, IColliderWithCC
{

    //public int selfSceneIndex = -1;
    public int targetSceneIndex;

    public IExitCondition mIExitCondition;

    public void OnPlayerCollisionEnter(GameObject player)
    {
        Debug.Log("ExitScript OnCollisionEnter()");
        if(mIExitCondition != null)
        {
            if (mIExitCondition.IsTriggerable())
            {
                SceneManager.LoadScene(targetSceneIndex, LoadSceneMode.Single);
            }
            else
            {
                Debug.Log("ExitScript 没有满足跳转条件");
            }
        }
        else
        {
            SceneManager.LoadScene(targetSceneIndex, LoadSceneMode.Single);
        }
    }

    public void OnPlayerCollisionExit(GameObject player)
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("ExitScript OnCollisionEnter()");
        //SceneManager.LoadScene(targetSceneIndex, LoadSceneMode.Single);
        //if (selfSceneIndex >= 0)
        //{
        //    SceneManager.UnloadSceneAsync(selfSceneIndex, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        //}
        
    }

}