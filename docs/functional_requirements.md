# Functional Requirements

## Roles

### Player

Player should be able to do the following:

- play in his VR scene with a HMI set
- interact with the scene with his hand sets independent of the Cue System at play
- (Optional) select an answer and send its result back to supervisor, whenever presented with a 'Poll' cue.

### Supervisor

Supervisor should be able to do the following:

- See the player's feed live, with as little lag and jitter as possible.
- Create a new cue, by setting the fields described in **Cue** section for each type.
- See the list of created cues, activate/deactivate them at anytime by a **checkbox**
- Update/Edit a cue's parameters by selecting the relevant cue from the list.
- Delete a cue from the list
- Save a local instance of the current cues list in the form of a json file.

There are two types of cues, when it comes to how they show up to the user. These are:

- **Context-Dependent:** These need to be placed in the scene by the supervisor. For this a 'Scene Edit Mode' is required (as described in the **Edit Mode** section)
- **Context-Independent:** These cues will always follow the  [posPlayer + (some bias in the z-axis)]. So they will occupy a fixed place in front of the user's point of view until they are hidden.


# Edit Mode


## Cues

### Cue Superclass

The supervisor sees a List\<Cue> from which he can do basic CRUD Operations.

- val cue_id: String
- var name: String
- val position: Vector3
- val transformation: Parent (can there be a cue that actually moves?)
- duration: float # The amount of time that the cue should be displayed after which point it disappears.
- var hideOnInteraction: bool
- Var Image : Background
- Var Color : Background Color

### Textbox

* Var Text: display text
* Var textColor: Color #he

### (Optional) Poll - Probably in the basic sense

- Var Toggles: RadioButttons or Toggles
- Var Text: Display text
- Var Image: Background
- Var Color: Background Color
- Var Color: Text Color
- Var Action: Callback action on clicking toggle
- Var Action: Callback action on clicking text

### Button

* Var TextBox: Button text
* Var CustomImage: custom image for button

### CustomImage

* Var Color: Color of the Image
* Var Action: Callback action on clicking the image

### Video


### Highlight

### Audio

Possibly every cue can have a audio mp3 file associated with it?
