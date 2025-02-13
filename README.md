# Spatial-temporal-precision-binding
Experiment #2: Spatial and temporal precision and binding
 To test our model by focusing on novel spatiotemporal episodic memory deficits accompanying bilateral MTL lesions.


Anticipated results:
    We expect that patients will show greater deficits in both spatial and temporal precision than controls, which will increase dramatically for 3 items vs. 1. This is because now all three items must be bound to each other and also in both space and time compared to single object/spatiotemporal binding.  This predicts a 3-way group by condition by number of objects interaction effect.  Specifically, both groups will perform worse at remembering the three items compared to one item but this will be more pronounced for the patients for remembering the three objects correctly in space and time compared to one object or any of these separately.


Alternative directions:
    Temporal compared to spatial precision may be more difficult for all participants, suggested in one report [107]; behavioral piloting can help match for difficulty across conditions.
<br />
<br />

## How to run the experiment:
Once you get the Spatial_temporal scene up, expand _Landmarks_ game object and click the LM_Experiment gameobject you will fill out the subjct name, number, and sex of the participant in the inspector panel.
Save, then run.
 <br />
 <br />

## Troubleshooting

If the experiment crashes or you for some reason or you had to stop the experiment, you can skip some of the tasks (Practice Trial, TASK_MainLoop, TASK_SpaceTime, and TASK_seq) and you can modify what trial to start at for each task by changing the Repeat Count variable from 1 to whatever trial they left off at or that you want them to start at.

You don't have to change the subject name/number as the code has it set to increment the number automatically to prevent overwriting.
<br />
<br />

## Unity Location names

LOCATIONS: (13-20 was added later, but worked with randomizing locations)
<pre>
1    20    4          7    19    10

13                               18

2          5          8          11

14                               17

3    15    6          9    16    12
</pre>
LEFT SIDE: (THE RIGHT SIDE IS MIRRORED BUT LABELED WITH _r_, ex: 10 - _r_top).

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
<br />
<br />
COLLIDERS:

front - first collider

fm - front-mid collider/second collider for the seq part

mid - middle collider/second collider for the first two parts

mm - middle-mid collider/fourth collider for the seq part

far - last collider
<br />
<br />

## Output Columns
**_output.csv**: This output is the first part of the project where the subjects are encording one item at a time in the hallway for either spatial or temporal (they still have to encode three items but not in one hall).
- Subject - Subject name and number

- Sex - Sex of the subject

- Block - Spatial or temporal retrieval (6 trials for each before they swap; repeat 5x)

- Trial - Trial number in the block

- LocationName - The name of the location the item appears at since they appear at random.

- LocationX - The x-coordinate in unity of the item

- LocationY - The y-coordinate in unity of the item

- LocationZ - The z-coordinate in unity of the item

- ResponseX - This is fixed to be the correct depth since the subject is placed where the collider is.

- ResponseY - The subject's y-coordinate response of where the item was on the screen using unity's Input.mousePosition and Camera.main.ScreenToWorldPoint().

- ResponseZ - The subject's z-coordinate response of where the item was on the screen using unity's Input.mousePosition and Camera.main.ScreenToWorldPoint().

- SpatialError - The distance error unity calculates with Vector2.Distance() between the object's y- and z-coordinates and the subject's y- and z-coordinate response

- GoalTime - The time the object appeared on the screen from the start.

- ResponseTime - The amount of time the subject pressed the space bar.

- TemporalError - The error between the GoalTime and the ResponseTime. Negative means undershot, positive been over shot.


**_SpaceTime_Output.csv**: This output is the second part of the project, where the subject sees three items in a hallway and has to encode both the spatial AND temporal location of the three objects.
- Subject - Subject name and number

- Sex - Sex of the subject

- Trial - Trial number (will be 30 as of 1/2025)

- Item#- The # item shown

- LocationX# - The x-coordinate of the # item

- LocationY# - The y-coordinate of the # item

- LocationZ# - The z-coordinate of the # item

- ResponseX# - This is fixed to be the correct depth since the subject is placed where the collider is in accordance to item #.

- ResponseY# - The subject's y-coordinate spatial response for the # item

- ResponseZ# - The subject's z-coordinate spatial response for the # item

- SpatialError# - The distance error unity calculates with Vector2.Distance() between the # object's y- and z-coordinates and the subject's # y- and z-coordinate response.

- GoalTime# - The time the # object appeared on the screen from the start.

- ResponseTime# - The amount of time the subject pressed the space bar for # item.

- TemporalError# - The error between the GoalTime# and the ResponseTime#. Negative means undershot, positive been over shot.


**_Sequence_Output**: This output is for the last part of the project where the subject has to remember the order of the 5 items shown in the hall way.
- Subject - Subject name and number

- Sex - Sex of the subject

- Trial - Trial number (will be 10 as of 1/2025)

- SeqItem# - The name of the # item

- ItemResponse# - The name of the # item the subject clicked on

- SeqError - How many items the subject got wrong
