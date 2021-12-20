using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserLightDeleteButton : MonoBehaviour
{
    private LightController lightController;
    private UserLight userLight;
    // Start is called before the first frame update
    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void Setup(LightController lightController, UserLight userLight)
    {
        this.lightController = lightController;
        this.userLight = userLight;
    }

    void OnClick()
    {
        lightController.DeleteUserLight(userLight);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
