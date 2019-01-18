import { Schema, arrayOf } from 'normalizr';

export const trail = new Schema('trails', { idAttribute: 'trailId' });
export const arrayOfTrails = arrayOf(trail);
