import Date from './Date'
import KPISelector from './KPISelector'
import { DateRange, ReactChange } from './Interfaces/Interfaces'
import GroupingSelector from './GroupingSelector'
import Interval from './IntervalSelector'

const Filters = ({
   onDateChange,
   onIntervalChange,
   onKPISelect,
   onGroupingChange,
   onDateTimeKeySelect,
   selectedKPIs,
   interval,
   dateTimeKeys,
   grouping,
}: {
   onDateChange: (date: DateRange) => void
   onIntervalChange: (interval: ReactChange) => void
   onKPISelect: (KPIs: ReactChange) => void
   onGroupingChange: (item: ReactChange) => void
   onDateTimeKeySelect: (selection: ReactChange) => void
   selectedKPIs: Map<string, boolean>
   interval: string
   dateTimeKeys: object
   grouping: string
}) => {
   return (
      <div className="flex gap-4 justify-around items-center px-28">
         <Date onDateChange={onDateChange} />
         <KPISelector {...{ selectedKPIs, onKPISelect }} />
         <Interval {...{ interval, onIntervalChange }} />
         <GroupingSelector
            {...{
               grouping,
               dateTimeKeys,
               onGroupingChange,
               onDateTimeKeySelect,
            }}
         />
      </div>
   )
}

export default Filters
