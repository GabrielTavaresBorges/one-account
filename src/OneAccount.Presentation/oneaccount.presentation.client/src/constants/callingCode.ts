// src/constants/callingCode.ts

export type CallingCode =
  | '+1'   //CAN or USA  (Canada or Russia)
  | '+7'   //KAZ or RUS  (Kazakstan or Russia)
  | '+20'  //EGY         (Egypty)
  | '+27'  //ZAF         (South Africa)
  | '+30'  //GRC         (Greece)
  | '+31'  //NLD         (Netherlands)
  | '+32'  //BEL         (Belgium)
  | '+33'  //FRA         (France)
  | '+34'  //ESP         (Spain)
  | '+36'  //HUN         (Hungary)
  | '+39'  //ITA         (Italy)
  | '+40'  //ROU         (Romania)
  | '+41'  //CHE         (Switzerland)
  | '+43'  //AUT         (Austria)
  | '+44'  //GBR         (Unit Kingdon)
  | '+45'  //DNK         (Denmark)
  | '+46'  //SWE         (Sweden)
  | '+47'  //NOR or SJM  (Norway or Svalvard and Jan Mayen))
  | '+48'  //POL         (Poland)
  | '+49'  //DEU         (Germany)
  | '+51'  //PER         (Peru)
  | '+52'  //MEX         (Mexico)
  | '+53'  //CUB         (Cuba)
  | '+54'  //ARG         (Argentina)
  | '+55'  //BRA         (Brazil)
  | '+56'  //CHI         (Chile)
  | '+57'  //COL         (Colombia)
  | '+58'  //VEN         (Venezuela)
  | '+60'  //MYS         (Malaysia)

export const callingCodeItems: Array<{ title: string; value: CallingCode }> = [
  { title: '+1', value: '+1' },
  { title: '+7', value: '+7' },
  { title: '+20', value: '+20' },
  { title: '+27', value: '+27' },
  { title: '+30', value: '+30' },
  { title: '+31', value: '+31' },
  { title: '+32', value: '+32' },
  { title: '+33', value: '+33' },
  { title: '+34', value: '+34' },
  { title: '+36', value: '+36' },
  { title: '+39', value: '+39' },
  { title: '+40', value: '+40' },
  { title: '+41', value: '+41' },
  { title: '+43', value: '+43' },
  { title: '+44', value: '+44' },
  { title: '+45', value: '+45' },
  { title: '+46', value: '+46' },
  { title: '+47', value: '+47' },
  { title: '+48', value: '+48' },
  { title: '+49', value: '+49' },
  { title: '+51', value: '+51' },
  { title: '+52', value: '+52' },
  { title: '+53', value: '+53' },
  { title: '+54', value: '+54' },
  { title: '+55', value: '+55' },
  { title: '+56', value: '+56' },
  { title: '+57', value: '+57' },
  { title: '+58', value: '+58' },
  { title: '+60', value: '+60' },
]
