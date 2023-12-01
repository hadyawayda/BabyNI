import Date from './Date'
import KPISelector from './KPISelector'
import { FilterProps } from './Interfaces/Interfaces'
import GroupingSelector from './GroupingSelector'
import Interval from './IntervalSelector'

const Filters = ({
   onDateChange,
   onIntervalChange,
   onKPISelect,
   onGroupingChange,
   onDateTimeKeyChange,
   selectedKPIs,
   interval,
   dateTimeKeys,
   grouping,
}: FilterProps) => {
   return (
      <div className="flex gap-4 justify-around items-center pl-24 pr-32">
         <Date onDateChange={onDateChange} />
         <KPISelector {...{ selectedKPIs, onKPISelect }} />
         <Interval {...{ interval, onIntervalChange }} />
         <GroupingSelector
            {...{
               grouping,
               onGroupingChange,
            }}
         />
      </div>
   )
}

export default Filters
