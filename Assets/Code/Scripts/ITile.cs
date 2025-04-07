using UnityEngine;

public interface ITile
{   
    void OnClicked();

    string Name { get; set; }
}
