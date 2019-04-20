import { Guid } from "guid-typescript";

export class BaseEntity {
  id: string

  constructor(entity) {
    for (let columnName in entity) {
      this[columnName] = entity[columnName];
    }
  }
}
