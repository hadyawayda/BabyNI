import {
   Chart,
   ChartTitle,
   ChartSeries,
   ChartSeriesItem,
   ChartCategoryAxis,
   ChartCategoryAxisItem,
} from '@progress/kendo-react-charts'
import { ChartComponentProps as Props } from './Interfaces/Interfaces'
import { useEffect, useState } from 'react'
import DateTimeKeySelector from './DateTimeKeySelector'

const ChartComponent = ({
   data,
   grouping,
   selectedKPIs,
   dateTimeKeys,
   onDateTimeKeyChange,
}: Props) => {
   // const [dateTimeKeys, setDateTimeKeys] = useState([])
   const [categories, setCategories] = useState(Object.keys(selectedKPIs))

   // useEffect(() => {
   //    const enabledCategories = Object.entries(selectedKPIs)
   //       .filter(([key, value]) => value) // Keep only entries where the value is true
   //       .map(([key]) => key) // Extract the keys

   //    setCategories(enabledCategories)
   // }, [selectedKPIs])

   // useEffect(() => console.log(categories), [selectedKPIs])

   return (
      <>
         <Chart className="w-11/12 mt-4">
            <div className="flex justify-around">
               <ChartTitle
                  margin={10}
                  text="Performance Chart"
               />
               <DateTimeKeySelector
                  {...{ dateTimeKeys, onDateTimeKeyChange }}
               />
            </div>
            <ChartCategoryAxis>
               <ChartCategoryAxisItem
                  title={{ text: 'KPIs' }}
                  categories={categories}
               />
            </ChartCategoryAxis>
            <ChartSeries>
               {/* {data.map((item, idx) => (
                  <ChartSeriesItem
                     key={idx}
                     type="line"
                     data={[1, 2, 3]}
                     name={`${item.DATETIME_KEY}  ${
                        grouping === 'NETYPE' ? item.NETYPE : item.NEALIAS
                     }`}
                  />
               ))} */}
            </ChartSeries>
         </Chart>
      </>
   )
}

export default ChartComponent

// {
//    /* <ul>
//          {Object.entries(selectedKPIs).map(([key, value], i) => (
//             <li key={i}>{value ? key : null}</li>
//          ))}
//       </ul> */
// }
// useEffect(() => {
//    console.log(selectedKPIs)
// }, [selectedKPIs])