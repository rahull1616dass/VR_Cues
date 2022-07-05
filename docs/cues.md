# Cue Superclass

The supervisor sees a List\<Cue> from which he can do basic CRUD Operations.

- val id: String
- var name: Stringst
- val position: Vector3
- val transformation: Transformation (can there be a cue that actually moves?)
- duration: float # The amount of time that the cue should be displayed after which point it disappears.

# Textbox

# Poll

- RadioButttons or Toggles
- 

# Image/Video


# Edit Mode

(Sooraj: Edit mode is similar to the supervision monitor, that allows the supervisor to manage and control existing cues by adding/altering parameters.)

## Cues

### Cue Superclass

The supervisor sees a List \<Cue> from which he can do basic CRUD Operations.

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

### (Sooraj) Prompts

- Context-Independent cues that alters the user about with any information, e.g.: "Do you want to stop the experience? Yes/No" or "Supervisor is requesting for streaming your VR feed. Proceed? Yes/No"

### (Sooraj) Haptic Signals

- Trigger haptic feedback through the handheld controllers upon necessary situations.

### (Sooraj) Ghost Objects

- Example: ghost hands in [VRescue](http://hci.uni-wuerzburg.de/projects/vrescue/)
- To indicate the process of doing an action
- Demonstration of correct movement etc

### (Sooraj) Alerts

### (Sooraj) Navigation Cues

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

## Cue positioning

(Sooraj: By definition, a cue's primary purpose is to alert or signal something to a person. For now, let's stick on to the second idea you mentioned. Let the cue be attached/placed near an object of interest. The job is done if it is visible to the player and the intended message is conveyed. Once this essential requirement is fulfilled, let's consider further enhancements if time permits.  )
