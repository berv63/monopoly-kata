# Monopoly TDD Kata

## iDesign https://www.idesign.net/
This kata code infrastructure follows the iDesign architecture pattern. However, it is not a strict iDesign application following all the directives of iDesign. The following are the main directives we will be looking at with this application:

1. Decompose based on volatility
2. Design iteratively, build incrementally
3. Design the project to build the system
4. Build the project along the critical path

### Managers
Managers are the heart of the control flow of the application. Nothing gets done unless a manager says so, and a manager should read in a logical step by step flow. The below example is a step by step overview of a players turn. Each of these steps can be broken down into the business requirements of those steps but the steps are unlikely to change. If the order of things/flow changes you know exactly where to look...The Manager.

Player turn example:
- Get board state
- Roll Dice
- Move
- Take action
- Update player turn
- Save board state

### Engines
Engines are where the business logic lies. This is to separate the flow of the application from the more intricate details of the requirements. During the flow the application doesn't care how the dice are rolled, just that they are and the results are passed to a future step. The business requirements can change without affecting the flow checks in the application this way. Once again if the business logic of a step changes you know exactly where to look...The Engine.

Roll Dice Example:
- Roll Dice 1 = math.rand(1-6)
- Roll Dice 2 = math.rand(1-6)

### Accessors
Accessors are used to separate out the data storage and general data access from the rest of the application. API calls, DB calls, Redis management, etc is typically done at the Accessor level. This is done for a few reasons. The volatility of the Accessors is very low. It's unlikely that in the course of a new application that an entire db set will be swapped out for a text file (for example). But in the event that it is in can be overhauled without affecting the remainder of the system. Also, accessors should be built in such a way that they are re-usable. If a DB/API change is required you know exactly where to look...The Accessor 

Get/Save State:
- Get Board state
- Serialize to object

### Overview
Managers are the entry point into the business logic of the application (non-UI/API logic). A manager should never call other managers, nor should engines call other engines. The managers should be viewed as volatile and throw away methods. If the flow changes, build a new manager method and then get rid of the old one and point your tests at the new method (if complex flow changes). For simple flow changes just rework the method. If a business logic changes, then you simply change it inline and everywhere that references that business logic will be updated. Have 4 managers that all calculate interest? Put it in an engine and share it with the 4 managers. It's usually a 1 line engine call and shouldn't matter that the 1 call is made in 4 different places. 

Managers should control when to update the state. You should not be afraid of unintended side affects of calling an engine. If an engine calls an accessor method it should only be to retrieve data, never to save.  

## TDD and iDesign
With c# specifically iDesign helps the TDD process by giving you logical places to mock your system boundaries. Usually you would want to mock at the HTTPClient level. But the reusable nature of the Accessors allows you to test them alone to the point where you know the only outcomes are tested, and then in the rest of the system you can mock those outcomes. Also if you find that you have pieces of your code that rely on untestable business logic (random elements) you can put those in their own engine and mock that method.
