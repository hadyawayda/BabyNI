export interface Data {
  datetimE_KEY: number
  time: Date
  networK_SID: number
  nealias: string
  netype: string
  rsL_INPUT_POWER: number
  maX_RX_LEVEL: number
  rsL_DEVIATION: number
}

export interface GridProps {
   props: Data[]
}

export type DateProps = {
   onDateChange: (date: string | null) => void
}

export interface DateProp {
   dateRange: string,
   startDate?: string,
   endDate?: string
}