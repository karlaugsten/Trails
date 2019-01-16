import { Schema, arrayOf } from 'normalizr';

export const trail = new Schema('trails');
export const arrayOfTrails = arrayOf(trail);
