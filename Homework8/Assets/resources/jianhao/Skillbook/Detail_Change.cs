using UnityEngine;
using System.Collections;

public class Detail_Change : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        detail();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void detail()
    {
        if (Property_Controller.my_Property == null)
            return;
        UILabel label = GetComponent<UILabel>();
        if (Property_Controller.my_Property.career == Career.Swordman)
        {
            GameObject temp = transform.Find("MP").gameObject;
            UILabel temp_label = temp.GetComponent<UILabel>();
            temp_label.text = Skill_Register.GetSkill("SwordSkill" + (DescriptionShow.current_click + 1)).mp_use + " mp";

            temp = transform.Find("CD").gameObject;
            temp_label = temp.GetComponent<UILabel>();
            temp_label.text = Skill_Register.GetSkill("SwordSkill" + (DescriptionShow.current_click + 1)).cd + " seconds";

            temp = transform.Find("Damage").gameObject;
            temp_label = temp.GetComponent<UILabel>();
            temp_label.text = Skill_Register.GetSkill("SwordSkill" + (DescriptionShow.current_click + 1)).damage_des + " [FFFFFF]: [FF0000]" + Skill_Damage.Instance.GetDamage("SwordSkill" + (DescriptionShow.current_click + 1)); ;

            temp = transform.Find("Description").gameObject;
            temp_label = temp.GetComponent<UILabel>();
            temp_label.text = Skill_Register.GetSkill("SwordSkill" + (DescriptionShow.current_click + 1)).des;
        }
        //label.text = "Lv" + Skill_Register.GetSkill("Sword" + name).skill_level;
        else if (Property_Controller.my_Property.career == Career.Magician)
        {
            GameObject temp = transform.Find("MP").gameObject;
            UILabel temp_label = temp.GetComponent<UILabel>();
            temp_label.text = Skill_Register.GetSkill("MagicSkill" + (DescriptionShow.current_click + 1)).mp_use + " mp";

            temp = transform.Find("CD").gameObject;
            temp_label = temp.GetComponent<UILabel>();
            temp_label.text = Skill_Register.GetSkill("MagicSkill" + (DescriptionShow.current_click + 1)).cd + " seconds";

            temp = transform.Find("Damage").gameObject;
            temp_label = temp.GetComponent<UILabel>();
            temp_label.text = Skill_Register.GetSkill("MagicSkill" + (DescriptionShow.current_click + 1)).damage_des + " [FFFFFF]: [FF0000]" + Skill_Damage.Instance.GetDamage("SwordSkill" + (DescriptionShow.current_click + 1)); ;

            temp = transform.Find("Description").gameObject;
            temp_label = temp.GetComponent<UILabel>();
            temp_label.text = Skill_Register.GetSkill("MagicSkill" + (DescriptionShow.current_click + 1)).des;
        }
    }
}