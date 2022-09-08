using TMPro;
using UnityEngine;

public class Login : MonoBehaviour
{
    [SerializeField] private TMP_InputField userPassword;
    [SerializeField] private TextMeshProUGUI textPassword;

    public void showPassword()
    {
        if (userPassword.contentType == TMP_InputField.ContentType.Password)
        {
            userPassword.contentType = TMP_InputField.ContentType.Standard;
            textPassword.fontSize = 20;
            textPassword.margin = new Vector4(0, 0, 0, 0);
        } else
        {
            userPassword.contentType = TMP_InputField.ContentType.Password;
            textPassword.fontSize = 35;
            textPassword.margin = new Vector4(0, 0, 0, -12);
        }

        userPassword.ForceLabelUpdate();
    }
}
