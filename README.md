```mermaid
erDiagram
  Employee }|--|{ Contract : ""
  Client ||--|{ Contract : ""
  Contract ||--|{ WorkPlan : ""
  Employee }|--|| Position : ""
  
  Employee{
      int Id
      string FirstName
      string LastName
      int PositionId
  }

  Client{
      int Id
      string Name
      enum CodeOfTheCountry
      string RegionCode
      strin SubscriberNumber
  }

  Contract{
      int Id
      string Subject
      string Address
      decimal Price
      int ClientId
  }

  WorkPlan{
      int Id
      DateTime DateStart
      DateTime DateFinish
      int ContractId
  }
  
  Position{
      int Id
      string Name
  }
  ```
  --------------------------------------------------
  ## 1. Domain
   >#### The domain layer contains entity definitions:
   >* Сlient
   >* Contract
   >* Employee
   >* Position
   >* WorkPlan
   >#### The relationship of entities to each other.
  ## 2. Persistence
   >* Performs mediation tasks between model layers and data display.
   >* Used by ORM Entity Framework Core.
   >* The method was used code first.
   >* A model was built using Fluent API methods.
  ## 3. Application
   >* Translating between external requests from the API(4) and Domain(1) logic (back and forth).
   >* Implementation of basic data operations (СRUD).
  ## 4. API
   >*
