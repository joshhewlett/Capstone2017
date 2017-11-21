/**
 * Imports here
 */
import users from './users';
import slides from './slides';
import presentations from './presentations';
import models from './3d_models';

export default {
    users,
    slides,
    presentations,
    models
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
