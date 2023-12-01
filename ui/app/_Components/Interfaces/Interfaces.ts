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
   data: chartProps
   grouping: string
   selectedKPIs: Map<string, boolean>
   dateTimeKeys: object
   onDateTimeKeyChange: (selection: ReactChange) => void
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
   selectedKPIs: Map<string, boolean>
}

export interface PageState {
   skip: number
   take: number
}

export interface FilterProps {
   onDateChange: (date: DateRange) => void
   onIntervalChange: (interval: ReactChange) => void
   onKPISelect: (KPIs: ReactChange) => void
   onGroupingChange: (item: ReactChange) => void
   onDateTimeKeyChange: (selection: ReactChange) => void
   selectedKPIs: Map<string, boolean>
   interval: string
   dateTimeKeys: object
   grouping: string
}

export interface GridComponentProps {
   data: gridProps
   selectedKPIs: Map<string, boolean>
   grouping: string
   dateTimeKeys: object
}

export interface GroupProps {
   onGroupingChange: (grouping: ReactChange) => void
   grouping: string
}

export interface IntervalProps {
   interval: string
   onIntervalChange: (e: ReactChange) => void
}
