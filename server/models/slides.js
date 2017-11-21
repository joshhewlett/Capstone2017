import {
    map
} from './index';

const schema = {
    id: {
        canBeNull: false,
        type: 'number'
    },
    sequence: {
        canBeNull: false,
        type: 'number'
    },
    presentation_id: {
        canBeNull: false,
        type: 'number'
    },
};


export default class {
    constructor(json) {
        map(schema, json);
    }
};
