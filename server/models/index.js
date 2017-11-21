/**
 * Imports here
 */
import users from './user';
import slides from './slides';

export default {
    users,
    slides
};

export let map = (schema, json) => {
    for (const item in schema) {
        const value = json[item];
        if (value) {
            if (typeof(value) !== item.type) {
                throw "Type mismatch!";
            }
        } else {
            if (!item.canBeNull) {
                throw "Can't be null";
            }
        }
        this.item = value;
    }
}
