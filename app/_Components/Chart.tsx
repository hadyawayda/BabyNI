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

const ChartComponent = ({ grouping, selectedKPIs, props }: Props) => {
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
         <Chart className="w-11/12">
            <ChartTitle text="Performance Chart" />
            <ChartCategoryAxis>
               <ChartCategoryAxisItem
                  title={{ text: 'KPIs' }}
                  categories={categories}
               />
            </ChartCategoryAxis>
            <ChartSeries>
               {props.map((item, idx) => (
                  <ChartSeriesItem
                     key={idx}
                     type="column"
                     data={[1, 2, 3]}
                     name={`${item.DATETIME_KEY}  ${
                        grouping === 'NETYPE' ? item.NETYPE : item.NEALIAS
                     }`}
                  />
               ))}
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
