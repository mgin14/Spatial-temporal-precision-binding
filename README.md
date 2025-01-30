# Spatial-temporal-precision-binding
 Experiment #2: Spatial and temporal precision and binding
 
 To test our model by focusing on novel spatiotemporal episodic memory deficits accompanying bilateral MTL lesions.

Anticipated results:
    We expect that patients will show greater deficits in both spatial and temporal precision than controls, which will increase dramatically for 3 items vs. 1. This is because now all three items must be bound to each other and also in both space and time compared to single object/spatiotemporal binding.  This predicts a 3-way group by condition by number of objects interaction effect.  Specifically, both groups will perform worse at remembering the three items compared to one item but this will be more pronounced for the patients for remembering the three objects correctly in space and time compared to one object or any of these separately.

Alternative directions:
    Temporal compared to spatial precision may be more difficult for all participants, suggested in one report [107]; behavioral piloting can help match for difficulty across conditions.

# How to run the experiment:
Once you get the Spatial_temporal scene up, expand _Landmarks game object and click the LM_Experiment gameobject you will fill out the subjct name, number, and sex of the participant in the inspector window.
Save, then run.

# Troubleshooting
If the experiment crashes or you for some reason had to stop the experiment, you can skip some of the tasks (Practice Trial, TASK_MainLoop, TASK_SpaceTime, and TASK_seq) and you can modify what trial to start at for each task by changing the Repeat Count variable from 1 to whatever trial they left off at or that you want them to start at.
