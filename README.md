# League Ranking Table Generator

## Problem Statement
Develop a command-line application that calculates the ranking table for a league. The application should be production-ready, maintainable, and testable.

## Input/Output Details
- **Input**: The application can accept input via stdin/stdout or by specifying filenames on the command line. The input will consist of the results of games, formatted one per line.
- **Output**: The output should list teams ordered from most to least points, adhering to a specified format.

## Rules
- **Scoring**: In the league:
  - A draw (tie) is worth 1 point.
  - A win is worth 3 points.
  - A loss is worth 0 points.
- Teams with the same number of points share the same rank and are listed in alphabetical order.

## Guidelines
- **Languages**: Preferably use Python, Java, Go, or Scala. If these are not comfortable, you may use Node.js, Ruby, or C.
- **Libraries**: You may use libraries installed via a common package manager (e.g., pip, npm, rubygems). It is not necessary to commit the installed packages.
- **Testing**: Include automated tests for your code.

## Setup
Document any complex setup steps required to run your solution.

## Platform Support
Ensure compatibility with Unix-like environments (e.g., OS X). If using a compiled language, please consider platform-agnostic aspects such as line-endings and file-path separators.

## Sample Input

```
Lions 3, Snakes 3
Tarantulas 1, FC Awesome 0
Lions 1, FC Awesome 1
Tarantulas 3, Snakes 1
Lions 4, Grouches 0
```


## Expected Output
```
Tarantulas, 6 pts
Lions, 5 pts
FC Awesome, 1 pt
Snakes, 1 pt
Grouches, 0 pts
```


# Solution
As required the solution is a console application developed in .NET v8. 

The application reads the scores from a file and prints in console the ranking due to the intructions above.

This project is using:
- C# language
- Interfaces
- Services
- Dependency Injection
- Nunit for unit testing
- Models
- Enums
- File reader
- App settings in config file
