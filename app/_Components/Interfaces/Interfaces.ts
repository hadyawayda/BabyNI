export interface gridData {
   DATETIME_KEY: number
   TIME: Date
   NETWORK_SID: number
   NEALIAS: string
   NETYPE: string
   RSL_INPUT_POWER: number
   MAX_RX_LEVEL: number
   RSL_DEVIATION: number
}

export interface chartData {
   DATETIME_KEY: number
   NETWORK_SID: number
   NEALIAS: string
   NETYPE: string
   RSL_INPUT_POWER: number
   MAX_RX_LEVEL: number
   RSL_DEVIATION: number
}

export type gridProps = gridData[]

export type chartProps = chartData[]

export interface DataProps {
   gridData: gridProps
   chartData: chartProps
}

export interface ChartComponentProps {
   chartData: chartProps
   grouping: string
   selectedKPIs: object
}

export interface DateProp {
   dateRange: string
   startDate?: string
   endDate?: string
}

export type ReactEvent = EventTarget & HTMLInputElement

export type ReactChange = React.ChangeEvent<HTMLInputElement>

export interface DateRange {
   start: string
   end: string
}

export type KPIProps = {
   onKPISelect: (KPIs: ReactChange) => void
   selectedKPIs: object
}
