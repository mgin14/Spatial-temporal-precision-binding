# Spatial-temporal-precision-binding
Experiment #2: Spatial and temporal precision and binding
 To test our model by focusing on novel spatiotemporal episodic memory deficits accompanying bilateral MTL lesions.


Anticipated results:
    We expect that patients will show greater deficits in both spatial and temporal precision than controls, which will increase dramatically for 3 items vs. 1. This is because now all three items must be bound to each other and also in both space and time compared to single object/spatiotemporal binding.  This predicts a 3-way group by condition by number of objects interaction effect.  Specifically, both groups will perform worse at remembering the three items compared to one item but this will be more pronounced for the patients for remembering the three objects correctly in space and time compared to one object or any of these separately.


Alternative directions:
    Temporal compared to spatial precision may be more difficult for all participants, suggested in one report [107]; behavioral piloting can help match for difficulty across conditions.


# How to run the experiment:
Once you get the Spatial_temporal scene up, expand _Landmarks_ game object and click the LM_Experiment gameobject you will fill out the subjct name, number, and sex of the participant in the inspector window.
Save, then run.
 

# Troubleshooting

If the experiment crashes or you for some reason or you had to stop the experiment, you can skip some of the tasks (Practice Trial, TASK_MainLoop, TASK_SpaceTime, and TASK_seq) and you can modify what trial to start at for each task by changing the Repeat Count variable from 1 to whatever trial they left off at or that you want them to start at.

You don't have to change the subject name/number as the code has it set to increment the number automatically to prevent overwriting.


# Vocabulary

Locations:

1____20____4__________7____19____10

13_______________________________18

2__________5__________8__________11

14_______________________________17

3____15____6__________9____16____12


LEFT SIDE: (THE RIGHT SIDE IS MIRRORED BUT LABELED WITH _r_, ex: 10 - _r_top). My vocab at the time was very rushed and limited and now it is too late, especially with modifications in the middle of development.
1  - _l_top
20 - _l_midmid_top
4  - _l_mid_top
13 - _l_top_mid
2  - _l_mid
5  - _l_mid_mid
14 - _l_bottom_mid
3  - _l_bottom
15 - _l_midmid_bottom
6  - _l_mid_bottom

Colliders:
front - first collider
fm - front-mid collider/second collider for the seq part
mid - middle collider/second collider for the first two parts
mm - middle-mid collider/fourth collider for the seq part
far - last collider
