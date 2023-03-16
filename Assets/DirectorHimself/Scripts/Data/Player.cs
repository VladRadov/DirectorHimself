using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Id { get; set; }

    public string Nickname { get; set; }

    public string Email { get; set; }

    public List<ICartoon> Cartoons { get; set; }
}