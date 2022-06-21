```mermaid
erDiagram
  Employee }|--|{ Contract : has
  Client ||--|{ Contract : has
  Contract ||--|| WorkPlan : contains
  Employee{
      int Id
      string Name
      string Surname
      string Position
  }

  Client{
      int Id
      string Name
      int ContactPhonNumber
  }

  Contract{
      int Id
      string Subject
      decimal Price
      int ClientId
      int WorkPlanId
      int EmployeeId
  }

  WorkPlan{
      int Id
      DataTime DataStart
      DataTime DataFinish
  }
  ```
