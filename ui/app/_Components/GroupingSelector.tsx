import { GroupProps } from './Interfaces/Interfaces'

const GroupingSelector = ({ onGroupingChange, grouping }: GroupProps) => {
   return (
      <>
         <div className="flex justify-center items-center gap-2">
            Grouping:
            <label>
               <input
                  type="radio"
                  name="grouping"
                  value="All"
                  className="whitespace-nowrap mr-2 align-middle"
                  checked={grouping === 'All'}
                  onChange={onGroupingChange}
               />
               All
            </label>
            <label>
               <input
                  type="radio"
                  name="grouping"
                  value="NETYPE"
                  className="whitespace-nowrap mr-2 align-middle"
                  checked={grouping === 'NETYPE'}
                  onChange={onGroupingChange}
               />
               NeType
            </label>
            <label>
               <input
                  type="radio"
                  name="grouping"
                  value="NEALIAS"
                  className="whitespace-nowrap mr-2 align-middle"
                  checked={grouping === 'NEALIAS'}
                  onChange={onGroupingChange}
               />
               NeAlias
            </label>
         </div>
      </>
   )
}

export default GroupingSelector
