import Grid from "./_Components/Grid";
import Chart from "./_Components/Chart";
import Navbar from "./_Components/Navbar";

export default async function Home() {
  const res = await fetch("https://localhost:7096/api/daily");
  const data = await res.json();
  console.log(data);

  return (
    <main className="flex min-h-screen h-full items-center justify-start p-4">
      <div className="w-96 flex justify-center items-start min-h-screen">
        <div className="mt-24 h-52">
          <Navbar />
        </div>
      </div>
      <div className="flex justify-around items-center min-h-screen w-full">
        <Grid props={data} />
        <Chart props={data} />
      </div>
    </main>
  );
}
