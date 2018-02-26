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
        if (!PolyMode) {
            base.OnInputClicked(eventData);
        } else {
            ///pManager.LoadThumbnailsPanel(4);
            pManager.LoadThumbnailsForward();
        }
    }

    // Assign incremental value.
    public override void Start() {
        base.Start();
        setAdjustmentValue(4);
    }
}
