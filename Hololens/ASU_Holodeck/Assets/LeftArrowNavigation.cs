using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class LeftArrowNavigation : ArrowNavigation {
    public override void setAdjustmentValue(int adjustment) {
        base.setAdjustmentValue(adjustment);
    }

    public override void OnInputClicked(InputClickedEventData eventData) {
        if (firstThumbnail > 1) {
            base.OnInputClicked(eventData);
        }
    }

    // Assign incremental value.
    public void Start() {
        setAdjustmentValue(-4);
    }
}
