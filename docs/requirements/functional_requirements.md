# Functional Requirements

## Roles

### Player

Player should be able to do the following:

- play in his VR scene with a HMI set
- interact and navigate across the scene with his hand sets independent of the Cue System at play
- interact with cues whenever necessary as specified in the cues section.

### Developer

Developer should be able to do the following:

- Interact with our Unity Package and define the structure of all the possible cues, by using the templates and the
  parameters that we define. Then customize each cue that will be displayed in the scene by:
  - Creating JSON representations, and/or
  - Manipulating ScriptableObject parameters within Unity
  - Save a local instance of the current cues list in the form of a JSON file
  - Save all non-JSON storable files such as mesh data, images etc. in the local directory and reference its path in the JSON file

## Cues

- **Poll**: Several types of questions in several pages shall be displayed. These questions can be of several types such as Likert Scale, Text Input, Radio Buttons, Sliders etc. The answers of thte player shall then be saved.
- **Prompt**: Single page questions shall pop up on the screen like Alert Dialogs with Yes/No answers such as,  "Do you want to stop the experience?" or "Supervisor is requesting for streaming your VR feed. Proceed?". The answers of thte player shall then be saved.
- **Audio**: An audio track (ideally the voice of a narrator for instance - like: [Stanley Parable Narrator](https://www.youtube.com/watch?v=I-pmnuU3DdY)) shall be played as the player interacts with the scene.
- **Alert**: It shall be displayed in text form to give the player warnings about the environment, situation or as a reminder.
- **Image**: It shall be displayed in front of the player's vision to give some instruction. (e.g. show which buttons to use in the controllers).
- **Haptic:** It shall trigger haptic feedback through the handheld controllers upon necessary situations.
- **Go Cue:** Steps or arrows shall be defined in the scene to lead the player to a certain direction.
- **Highlight:** The object's outline shall be highlighted with another color to draw player's attention.
- **Ghost Object:** The player is instructed to perform a certain action via seeing that action mimicked for him (e.g. Ghost Hands grabbing a door handle.)

### Triggers

The cues can be active/inactive at any given time. The activity can be defined to be triggered in the following ways:

- **Position Trigger:** Whenever the player steps inside a certain area
- **Time Trigger:** After a set amount of time passes after the game has started
