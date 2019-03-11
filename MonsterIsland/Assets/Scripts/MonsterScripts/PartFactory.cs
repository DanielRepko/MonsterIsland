using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class PartFactory : MonoBehaviour {

	//public static HeadPart GetHeadPart(string monsterName)
 //   {
 //       HeadPartInfo partInfo = Helper.GetHeadPartInfo(monsterName);
 //       HeadPart headPart = new HeadPart(partInfo);

 //       return headPart;
 //   }

    public static HeadPartInfo GetHeadPartInfo(string monsterName)
    {
        XmlDocument mainSprite = new XmlDocument();
        mainSprite.Load("Assets/Resources/Sprites/Monsters/" + monsterName + "/Head/Monster_" + monsterName + "_Head_Face_idle.svg");
        XmlDocument neckSprite = new XmlDocument();
        neckSprite.Load("Assets/Resources/Sprites/Monsters/" + monsterName + "/Head/Monster_" + monsterName + "_Head_neck.svg");
        XmlDocument hurtSprite = new XmlDocument();
        hurtSprite.Load("Assets/Resources/Sprites/Monsters/" + monsterName + "/Head/Monster_" + monsterName + "_Head_Face_hurt.svg");
        XmlDocument attackSprite = new XmlDocument();
        attackSprite.Load("Assets/Resources/Sprites/Monsters/" + monsterName + "/Head/Monster_" + monsterName + "_Head_Face_attack.svg");

        HeadPartInfo headPart = new HeadPartInfo()
        {
            monster = monsterName,
            mainSprite = mainSprite.InnerXml,
            neckSprite = neckSprite.InnerXml,
            hurtSprite = hurtSprite.InnerXml,
            attackSprite = attackSprite.InnerXml
        };
        return headPart;
    }

}
