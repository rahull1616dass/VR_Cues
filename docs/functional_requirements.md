# Functional Requirements

## Roles

### Player

Player should be able to do the following:

- play in his VR scene with a HMI set
- interact with the scene with his hand sets independent of the Cue System at play
- select an answer and send its result back to supervisor, whenever presented with a 'Poll' cue.

### Supervisor

Supervisor should be able to do the following:

- See the player's feed live, with as little lag and jitter as possible.
- Create a new cue, by setting the fields described in **Cue** section for each type.
- See the list of created cues, activate/deactivate them at anytime by a **checkbox**
- Update/Edit a cue's parameters by selecting the relevant cue from the list.
- Delete a cue from the list

There are two types of cues, when it comes to how they show up to the user. These are:

- **Context-Dependent:** These need to be placed in the scene by the supervisor. For this a 'Scene Edit Mode'

## Cues

### Cue Superclass

The supervisor sees a List\<Cue> from which he can do basic CRUD Operations.

- val id: String
- var name: Stringst
- val position: Vector3
- val transformation: Transformation (can there be a cue that actually moves?)
- duration: float # The amount of time that the cue should be displayed after which point it disappears.

### Textbox

### Poll

- RadioButttons or Toggles
- 

### Image

### Video
