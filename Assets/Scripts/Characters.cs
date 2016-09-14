using UnityEngine;
using System.Collections;

public class Characters : MonoBehaviour
{

    public class info
    {
        public int id;
        public string name;    
        public info(int _id, string _name)
        {
            id = _id;
            name = _name;
        }
    }
    public enum Ids
    {
        Player,
        Eric,
        Mulher,
        Filha
    }

    public static info player = new info((int)Ids.Player, "Viktor");
    public static info eric = new info((int)Ids.Eric, "Eric");

    public static info mulher = new info((int)Ids.Mulher, "Geraldete"); // mulher
    public static info filha = new info((int)Ids.Filha, "Valesca");// filha

 
}
