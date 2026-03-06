// src/assets/flags/index.ts

import argFlag from '@/assets/flags/Argentina.svg' // Argentina
import autFlag from '@/assets/flags/Austria.svg' // Austria
import belFlag from '@/assets/flags/Belgium.svg' // Belgium
import braFlag from '@/assets/flags/Brazil.svg' // Brazil
import canFlag from '@/assets/flags/Canada.svg' // Canada
import cheFlag from '@/assets/flags/Switzerland.svg' // Switzerland
import chiFlag from '@/assets/flags/Chile.svg' // Chile
import colFlag from '@/assets/flags/Colombia.svg' // Colombia
import cubFlag from '@/assets/flags/Cuba.svg' // Cuba
import deuFlag from '@/assets/flags/Germany.svg' // Germany
import dnkFlag from '@/assets/flags/Denmark.svg' // Denmark
import egyFlag from '@/assets/flags/Egypt.svg' // Egypt
import espFlag from '@/assets/flags/Spain.svg' // Spain
import fraFlag from '@/assets/flags/France.svg' // France
import gbrFlag from '@/assets/flags/England.svg' // England
import grcFlag from '@/assets/flags/Greece.svg' // Greece
import hunFlag from '@/assets/flags/Hungary.svg' // Hungary
import itaFlag from '@/assets/flags/Italy.svg' // Italy
import kazFlag from '@/assets/flags/Kazakhstan.svg' // Kazakhstan
import mexFlag from '@/assets/flags/Mexico.svg' // Mexico
import mysFlag from '@/assets/flags/Malaysia.svg' // Malaysia
import nldFlag from '@/assets/flags/Netherlands.svg' // Netherlands
import norFlag from '@/assets/flags/Norway.svg' // Norway
import perFlag from '@/assets/flags/Peru.svg' // Peru
import polFlag from '@/assets/flags/Poland.svg' // Poland
import rouFlag from '@/assets/flags/Romania.svg' // Romania
import rusFlag from '@/assets/flags/Russia.svg' // Russia
import sjmFlag from '@/assets/flags/Norway.svg' // Svalbard and Jan Mayen
import sweFlag from '@/assets/flags/Sweden.svg' // Sweden
import usaFlag from '@/assets/flags/United_States.svg' // United States
import venFlag from '@/assets/flags/Venezuela.svg' // Venezuela
import zafFlag from '@/assets/flags/South_Africa.svg' // South Africa

// exports individuais (se quiser importar direto)
export {
  argFlag,
  autFlag,
  belFlag,
  braFlag,
  canFlag,
  cheFlag,
  chiFlag,
  colFlag,
  cubFlag,
  deuFlag,
  dnkFlag,
  egyFlag,
  espFlag,
  fraFlag,
  gbrFlag,
  grcFlag,
  hunFlag,
  itaFlag,
  kazFlag,
  mexFlag,
  mysFlag,
  nldFlag,
  norFlag,
  perFlag,
  polFlag,
  rouFlag,
  rusFlag,
  sjmFlag,
  sweFlag,
  usaFlag,
  venFlag,
  zafFlag,
}

// mapa (ótimo pra resolver por código)
export const flagsByIso3 = {
  ARG: argFlag,
  AUT: autFlag,
  BEL: belFlag,
  BRA: braFlag,
  CAN: canFlag,
  CHE: cheFlag,
  CHI: chiFlag,
  COL: colFlag,
  CUB: cubFlag,
  DEU: deuFlag,
  DNK: dnkFlag,
  EGY: egyFlag,
  ESP: espFlag,
  FRA: fraFlag,
  GBR: gbrFlag,
  GRC: grcFlag,
  HUN: hunFlag,
  ITA: itaFlag,
  KAZ: kazFlag,
  MEX: mexFlag,
  MYS: mysFlag,
  NLD: nldFlag,
  NOR: norFlag,
  PER: perFlag,
  POL: polFlag,
  ROU: rouFlag,
  RUS: rusFlag,
  SJM: sjmFlag,
  SWE: sweFlag,
  USA: usaFlag,
  VEN: venFlag,
  ZAF: zafFlag,
} as const

export type Iso3FlagCode = keyof typeof flagsByIso3
