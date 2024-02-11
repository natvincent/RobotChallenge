# Robot Coding Challenge

## Assumptions and Thoughts
### I/O
- Need to handle input from StdIn, output to StdOut. 
  - This allows input from console or from a file. 
  - Allows for easier integration testing
- Use stdin read line initially, although this may have to be rethought to handle very long input lines that could bork the input handling
- It's worthwhile abstracting the input and output handling as I/O could be changed abitrarily e.g. JSON in, HTML out
### Commands
- Bail out as early as possible when parsing commands
  - Naive approch is to delimit on space for command then by comma for parameters (simplest thing that works). 
  - Eventual robust/performant handling of commands should be done with the knowledge of the longest possible string for command name and arguments (including the command itself).
  - Regex will work for this. If it's not performant, a hand coded parser *might* be needed, but only if profiling shows that's the slow part.
- Initial thoughts were that commands should be objects created for each command string from a factory based on the command name.
  - But on thinking, using something like the strategy pattern is probably more performant.
  - Create commands on startup and add them to a list or dictionary.
  - List item should contain action method and regex for parameters
  - Given the small number of commands, an in-order array lookup is reasonable to start with (rather than an expensive dictionary).
- Command function should take parameters, Table Top and Robot
## Inital Design
### Command Dispatcher 
- Depends on Table Top and Robot Factory.
- Robot is initially either null or null object. Maybe have a `IsRobotPlaced()` method
- Bail early in command processing if `IsRobotPlaced()` is `false`.
### Table Top
- Assume that a table top can be started with any size, or can be resized during session.
  - What happens if existing robot location is invalid after table top size has changed? 
- One method to test if a move-to location is valid - `CanMoveTo()`.
  - Initially just checks the bounds
  - Can be extended to check for obstacles. 
### Robot
- Keeps track of it's own location and heading

## Possible Extentions in Timed Challenge
- Table top size specified on command line.
- Obstacals
### Commands
- Change table top size - TABLE *width,height*
- Diagonal headings.
- MOVE *squares*
- MOVETO *x,y*
  - Maybe a metacommand that generates subcommands? A bit of path finding?
- REPORT robot location as an ASCII grid, markdown or HTML table, JSON.
- Input via JSON.
- Make it a web service?