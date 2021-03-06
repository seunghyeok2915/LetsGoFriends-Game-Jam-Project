using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddRankPage : MonoBehaviour
{
    private InputField nameField;
    private Button addBtn;

    private void Start()
    {
        nameField = Utils.Bind<InputField>(gameObject, "NameInputField");
        addBtn = Utils.Bind<Button>(gameObject, "AddRankBtn");

        addBtn.onClick.AddListener(AddRank);
    }

    private void AddRank()
    {
        if (nameField.text.Trim() == "")
        {
            print("이름이 비어잇음");
            return;
        }

        RankDBManager.Instance.AddRank(nameField.text, GameManager.Instance.sumScore);
        GameManager.Instance.showRankPage.OpenPage();
        gameObject.SetActive(false);
    }

}
