using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IColorReceiver
{
    void PreviewColor(Color color);
    void Cancel();
    void SetColor(Color color);
}
