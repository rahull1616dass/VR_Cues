## Background: 
  VR applications are commonly used in the healthcare context. For example in an exposure therapy to learn how to cope with a specific anxiety or in a gait rehabilitation application that can be applied after someone suffered from a stroke. Applications like these often include a supervisor who is monitoring and adapting the VR-therapy session. In order to do this, the supervisor must be able to constantly assess the current state of the client. One way to obtain an assessment of the user's condition would be through subjective feedback by the client, i.e. questionnaires. Dependent variables that are assessed via questionnaires would be anxiety (e.g. in exposure therapy), cognitive workload [5] (e.g. in training/learning applications), or cybersickness [4] (could actually be relevant in a lot of different VR applications).
  Thus, in this project we would like to build a tool for Unity that enables a supervisor to create his/her own questionnaires and display them in VR. For more detailed information about the motivation of this project, have a look at [1].

  This is not only relevant in the realm of therapy and training. A lot of VR research revolves around the idea to gain insights into subjectiv feelings and the general expereience that is elicted by an VR application. There are qustionnaires that are quite specific to VR, e.g. Presence Questionnaires [6, 7], or body ownership [8].
  
  If you need general insights on how the questionnaires look like that we are typically using in the research of our chair: We have a GitLab wiki in which we collect those questionnaires with summary of their items, execution, and analysis. If you think that might be intersting for you, just send me a message and I will give you access. 
  
  Most questionnaires, however, contain Likert type questions. These are questions that use statements and a respondent then indicates how much they agree or disagree with that statement. Usually, the questions are answered on a scale of 1 to 7 or 1 to 5 (even though there are still a lot of differences here). Usually, there are anchors associated with the numbers, e.g. *Strongly disagree, Disagree, Somewhat disagree, Neither agree nor disagree, Somewhat agree, Agree, Strongly agree*. The answers to the question would basically be a radio-button menu. 

  In the end, the creation and triggering of questions should not necessarily be done by Unity itself, but should come from outside. In this case, an API would be needed in Unity to communicate these things. 

## Tasks

This is how the base functionality could look like (basically a CRUD process for a questionnaire). Of course all of this can be adapted during the runtime of the project: 

- A supervisor can for example provide a set of statements. And then can decide on which scale this statements should be rated. And then could provide some anchors. For example a 7 point scale would then be from Full Disagreement to Full Agreement. 

- This questions could be stored on the local drive (e.g. json, xml) and loaded. 

- And this questionnaire and its statements should then be transferred on a 3D plane in VR where a user could just provide a rating to every question

- When the questionnaire is finished, the answers would be stored.

  
This basic functionality can be massiveley extended (depending on where you want to lay the focus of your project and how much time and effort you can spend on it). For example, a lof of other question types are conceivable, different questions could form question groups, different groups could be combined to yield new questionnaires, randomization of the order of the questions, images and videos could be included into questions, etc...

On our chair we are using LimeSurvey, which is a web-based quesionnaire administration tool. It actually has quite some similar functionality (of course much more extensive than what we are aiming at here). However, there you could get some inspiration how they are doing things, e.g. which question types they are offering and how questions and questionnaires are created, or how they handle the saving and loading of questions, questionnaires, and answers (we might even adopt their format). If you want, I can give you access to LimeSurvey, so you can browse it and get the info you need. 

## Update (10.06.2022)

In the last meeting we had a more concrete discussion about the scope of this project. The main conclusion was, that you would just be responsible for handling the Cues in the 3D envrionment. The way these Cues reach your API is not of interst (so far). 
The base task for the questionnaires that I would imagine for this would look like this: 
You will create a Class representation for Questionnaires and Questions. The first question-type that you would implement are Likert-type questions. They consist of a statement and a Point-Scale that you one can use to express how much one would agree to this statement. Attache to the points can be anchors that represent the meaning of the points. Every point can have an anchor but doesn???t have to have one. However, at least the first and the last one need an anchor, e.g. (strongly agree strongly disagree).
Usually a questionnaire would have a (short) intro text at the top that explains how it works and what???s its purpose.
Your API should be able to handle that information and display the questionnaire on a 3D plane in the virtual environment. 
The user in VR should be able to anser these question via controller interaction 

When the user is finsihed with answering the questionnaire, the answers should be stored.

You can adapt this and realize this as it suits you best. Later on in the project we can extend this, e.g. with more question types, etc...

## References

  [1] Halbig, A., Babu, S. K., Gatter, S., Latoschik, M. E., Brukamp, K., & von Mammen, S. (2022). Opportunities and Challenges of Virtual Reality in Healthcare???A Domain Experts Inquiry. Frontiers in Virtual Reality, 3, 837616.


  [4] Kennedy, R. S., Lane, N. E., Berbaum, K. S., & Lilienthal, M. G. (1993). Simulator sickness questionnaire: An enhanced method for quantifying simulator sickness. The international journal of aviation psychology, 3(3), 203-220.

  [5] Hart, S. G., & Staveland, L. E. (1988). Development of NASA-TLX (Task Load Index): Results of empirical and theoretical research. In Advances in psychology (Vol. 52, pp. 139-183). North-Holland.

  [6] Bouchard, S., Robillard, G., St-Jacques, J., Dumoulin, S., Patry, M. J., & Renaud, P. (2004, October). Reliability and validity of a single-item measure of presence in VR. In The 3rd IEEE international workshop on haptic, audio and visual environments and their applications (pp. 59-61). IEEE.

  [7] Schubert, T., Friedmann, F., & Regenbrecht, H. (2001). The experience of presence: Factor analytic insights. Presence: Teleoperators & Virtual Environments, 10(3), 266-281.

  [8] Roth, D., & Latoschik, M. E. (2019). Construction of a validated virtual embodiment questionnaire. arXiv preprint arXiv:1911.10176.
