using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class RightArrowNavigation : ArrowNavigation {
    public override void setAdjustmentValue(int adjustment) {
        base.setAdjustmentValue(adjustment);
    }

    public override void OnInputClicked(InputClickedEventData eventData) {
        // TODO: Set maximum value for going to the right to check.
        //if (firstThumbnail < MAX)
        base.OnInputClicked(eventData);
    }

    // Assign incremental value.
    public void Start() {
        setAdjustmentValue(4);
    }
}
