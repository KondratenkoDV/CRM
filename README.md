```mermaid
erDiagram
  Employee }|--|{ Client : has
  Client ||--|{ Contract : has
  Employee }|--|{ Contract : has
  Contract ||--|| WorkPlan : contains
  Employee{
      int id
      string Name
      string Surname
      string Position
  }

  Client{
      int id
      string Name
      int ContactPhonNumber
  }

  Contract{
      int Id
      string Subject
      decimal Price
  }

  WorkPlan{
      int id
      DataTime DataStart
      DataTime DataFinish
  }
  ```
