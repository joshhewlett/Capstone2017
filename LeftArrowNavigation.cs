using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class LeftArrowNavigation : ArrowNavigation {
    public override void setAdjustmentValue(int adjustment) {
        base.setAdjustmentValue(adjustment);
    }

    public override void OnInputClicked(InputClickedEventData eventData) {
        if (!PolyMode && firstThumbnail > 1) {
            base.OnInputClicked(eventData);
        } else if (pManager.thumbnailCounter > 0) {
            //pManager.LoadThumbnailsPanel(-4);
            pManager.LoadThumbnailsBack();
        }
    }

    // Assign incremental value.
    public override void Start() {
        base.Start();
        setAdjustmentValue(-4);
    }
}
