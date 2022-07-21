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
