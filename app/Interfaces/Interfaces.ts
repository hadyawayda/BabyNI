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

export type Props = Data[]

export interface GridProps {
   props: Props
}

export interface DateProp {
   dateRange: string
   startDate?: string
   endDate?: string
}

export interface ChartProps {
   grouping: string
   props: Data[]
   selectedKPIs: object
}

export type ReactEvent = EventTarget & HTMLInputElement

export type ReactChange = React.ChangeEvent<HTMLInputElement>
