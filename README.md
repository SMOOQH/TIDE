# TIDE

This took me maybe 2 hours in total. Kinda dipped halfway through to go for a walk in this nice weather 😃
I tried not to spend a lot of time on it, as, well, if you spend a week on it, you can make something really good in that time, so I wanted to do what I can do off the "cuff" as such.

Everything I did was kept simple, I didn't try to mess about with things I didn't know as I wouldn't really know what I'm doing.
I was temped to try AngularJs for this but I am fairly rusty in it, and if I'm being honest, I'd rather have someone sit with me and walk me through it so I can grasp it that way. It tends to be how I learn.

Architecture is super simple.
Main Website.
A Shared Models Project.
Service Project.
Test Project.

If there was an actual database for me to use, I'd have made a seperate database service project in addition to the above. Just to keep it a bit seperate from the main "helpers".

In terms of design, it's just bog standard bootstrap, turn it into a "dark mode" version, as I'm a dev and darker the better 😉
I think it highlights the difference in warning a bit easier too, as I demonstrated a bit more obviously through the border outline gradients.

Testing was super simple too, just NUnit and FakeItEasy. Only tested the BusinessService, as this was the only real thing to be worth tested. ApiService really didn't do much other than make a request.

Could've done the whole input and sumit in a form, but I wanted to show I can definitely do some JavaScript as I know you guys do use Javascript Frameworks. May as well show I have some understanding of it! 

I was tempted to put an easter egg in but I'll save that for another day. 😁

Logging was done via SeriLog, just a simple one liner and it'll log everything, both requests and responses.

So yeah, like I said, I didn't want to spend loads of time on this, there's loads I want to do on this but I'd rather not get carried away.

Thanks! ☀
Sameer Q. Hussain
