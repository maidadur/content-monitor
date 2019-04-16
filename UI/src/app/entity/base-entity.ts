import { Guid } from "guid-typescript";

export class BaseEntity {
  id: Guid

  constructor(entity) {
    for (let columnName in entity) {
      this[columnName] = entity[columnName];
    }
  }
}
