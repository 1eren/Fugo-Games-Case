using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FugoUserModel
{
    private int _id;
    public int ID { get => _id; set => _id = value; }
    public bool isActive { get; set; }
    public string name { get; set; } = null!;

    public string city { get; set; } = null!;
    public string country { get; set; } = null!;
    public string email { get; set; }
    public string password { get; set; }

    public float FugoCoin { get; set; }
    public int winCount { get; set; }
    public int loseCount { get; set; }

}
public class UpdateFugoUserModel
{
    public string adres { get; set; } = null!;
    public bool isActive { get; set; }

    public string name { get; set; } = null!;
    public string city { get; set; } = null!;
    public string country { get; set; } = null!;
    public string email { get; set; }
    public string password { get; set; }

    public float FugoCoin { get; set; }
    public int winCount { get; set; }
    public int loseCount { get; set; }
}
