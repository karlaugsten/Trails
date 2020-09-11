import React from 'react';
import Input from './Input';
import MyButton from './Button';

export const Button = MyButton;

export const TextInput = (props) => <Input {...props} type="text" />

export const PasswordInput = (props) => <Input {...props} type="password" />

const NumberConversion = (event) => NumberValidation(event.target.value) ? ({
    target: {
      name: event.target.name,
      type: 'number',
      value:  Number(event.target.value)
    }
  }) : event;

const NumberValidation = (num) => {
  return !isNaN(Number(num));
}

export const NumberInput = ({onChange, ...others}) => <Input validate={NumberValidation} {...others} onChange={(val) => onChange(NumberConversion(val))} type="text" />
